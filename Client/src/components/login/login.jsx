import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../../services/auth";

const Login = () => {
  const navigate = useNavigate();

  const [userlogin, setUserlogin] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(null);
    setLoading(true);

    try {
      await login(userlogin, password);
      navigate("/test");
    } catch (err) {
      console.error(err);
      setError("Invalid credentials");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h1>Login page</h1>
      <form onSubmit={handleSubmit}>
        <label>Login</label>
        <input value={userlogin} onChange={(e) => setUserlogin(e.target.value)}/>
        <label>Password</label>
        <input type="password" value={password} onChange={(e) => setPassword(e.target.value)}/>
        {error && <p>{error}</p>}
        <button type="submit" disabled={loading}>
          {loading ? "Logging in..." : "Log in"}
        </button>
      </form>
    </div>
  );
};

export default Login;