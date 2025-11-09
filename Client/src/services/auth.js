import api from "./api";

export const login = async (login, password) => {
  try {
    const res = await api.post("/auth/login", { login, password });
    localStorage.setItem("token", res.data.token);
    console.log("Logged in successfully");
    return res.data;
  } catch (err) {
    console.error("Login failed:", err);
    throw err;
  }
};