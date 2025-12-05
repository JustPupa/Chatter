import { api, setAccessToken } from "./api";

export async function login(login, password) {
    const res = await api.post("/auth/login", { login, password });
    setAccessToken(res.data.accessToken);
    return res.data;
}

export async function refreshToken() {
    try {
        const res = await api.post("/auth/refresh");
        const newToken = res.data.accessToken;
        setAccessToken(newToken);
        return newToken;
    } catch {
        console.warn("Refresh failed");
        return null;
    }
}

export function logout() {
    setAccessToken(null);
    api.post("/auth/logout");
}