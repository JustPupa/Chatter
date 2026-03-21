import { api } from "./api";

export const loadUser = async (setUser) => {
    try {
      const response = await api.get("/user/account", {
        withCredentials: true
      });
      setUser(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const logout = async () => {
    try {
      if (!window.confirm("Do you really want to logout?")) return;
      const response = await api.post("/auth/logout", {
        withCredentials: true
      });
      localStorage.removeItem("accessToken");
      window.location.href = "/login";
    } catch(e) {
        console.error(e);
    }
}

export const getUserChats = async (pageNum, pageSize) => {
    try {
      const response = await api.get("/Chat/My", {
        params: {
            pageNumber: pageNum,
            pageSize: pageSize
        },
        withCredentials: true
      });
      return response.data;
    } catch(e) {
        console.error(e);
    }
}

export const getChatMessages = async (chatid, pageNum, pageSize) => {
    try {
      const response = await api.get(`/Message/${chatid}/messages`, {
        params: {
            pageNumber: pageNum,
            pageSize: pageSize
        },
        withCredentials: true
      });
      return response.data;
    } catch(e) {
        console.error(e);
    }
}

export const getUserFeed = async (pageNum, pageSize) => {
    try {
      const response = await api.get("/SMPost/latest", {
        withCredentials: true
      });
      return response.data;
    } catch(e) {
        console.error(e);
    }
}