import { createContext, useState, useEffect } from "react";
import axios from "../api/axios";

export const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
    const [token, setToken] = useState(null);

    useEffect(() => {
        const refresh = async () => {
            try {
                const response = await axios.post("/auth/refresh", {}, { withCredentials: true });
                setToken(response.data.accessToken);
            } catch (err) {
                setToken(null);
            }
        };

        refresh();
    }, []);

    const login = async (username, password) => {
        const response = await axios.post(
            "/auth/login",
            { username, password },
            { withCredentials: true }
        );
        setToken(response.data.accessToken);
    };

    const logout = async () => {
        setToken(null);
        await axios.post("/auth/logout", {}, { withCredentials: true });
    };

    return (
        <AuthContext.Provider value={{ token, setToken, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};