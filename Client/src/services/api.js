import axios from "axios";
import { refreshToken } from "./auth";

let accessToken = null;

export function setAccessToken(token) {
    accessToken = token;
}

export function getAccessToken() {
    return accessToken;
}

export const api = axios.create({
    baseURL: "http://localhost:31994/api",
    withCredentials: true
});

api.interceptors.request.use(
    config => {
        if (accessToken) {
            config.headers["Authorization"] = `Bearer ${accessToken}`;
        }
        return config;
    },
    error => Promise.reject(error)
);

api.interceptors.response.use(
    res => res,
    async error => {
        const original = error.config;
        if (original._retry) return Promise.reject(error);
        if (original.url.includes("/auth/refresh")) {
            setAccessToken(null);
            return Promise.reject(error);
        }
        if (error.response?.status === 401) {
            original._retry = true;
            const newToken = await refreshToken();
            if (!newToken) return Promise.reject(error);
            setAccessToken(newToken);
            original.headers["Authorization"] = `Bearer ${newToken}`;
            return api(original);
        }
        return Promise.reject(error);
    }
);