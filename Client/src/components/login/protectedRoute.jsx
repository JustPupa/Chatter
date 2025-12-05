import { Navigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { refreshToken } from "../../services/auth";
import { getAccessToken } from "../../services/api";

const ProtectedRoute = ({ children }) => {
  const [authorized, setAuthorized] = useState(null);

  useEffect(() => {
    const checkAuth = async () => {
      const token = getAccessToken();
      if (token) {
        setAuthorized(true);
        return;
      }
      const newToken = await refreshToken();
      if (newToken) {
        setAuthorized(true);
      } else {
        setAuthorized(false);
      }
    };
    checkAuth();
  }, []);
  if (authorized === null) return <div>Загрузка...</div>;
  if (!authorized) return <Navigate to="/login" replace />;
  return children;
};

export default ProtectedRoute;