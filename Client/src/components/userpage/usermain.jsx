import { useEffect, useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { loadUser } from '../../services/requests';
import '../../styles/userpage.css';

function UserMain() {
  const navigate = useNavigate();
  const [user, setUser] = useState(null);

  useEffect(() => {
    loadUser(setUser);
  }, []);

  if (!user) return <div>Loading...</div>;

  return (
    <div>
      <div>
        <h1>User account</h1>
        <p>ID: {user.id}</p>
        <p>Login: {user.login}</p>
      </div>
    </div>
  );
}

export default UserMain;