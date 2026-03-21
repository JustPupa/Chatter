import { NavLink } from "react-router-dom";

function SidebarLink({to, icon, label, top}) {
  return (<NavLink to={to} className={({ isActive }) =>
      `fixed ${top} left-5 w-[25%] flex items-center gap-[0.5vw] p-[0.5vw] rounded-md transition-all" ${
        isActive ? "bg-gray-300 text-white" : "bg-white text-white"
      }`
    }
  >
    <img src={icon} className="w-[3vw] mr-1" alt={label} />
    <span className="text-[2vw] text-black">{label}</span>
  </NavLink>
  );
}

export default SidebarLink;