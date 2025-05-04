import React from 'react';
import { useState } from 'react';
import { NavLink } from 'react-router-dom';

function Navbar() {
  const [menuActive, setMenuActive] = useState(false);

  const toggleMenu = () => {
    setMenuActive(!menuActive);
  };

  return (<>
    <nav>
      <button id="mobile-menu" onClick={toggleMenu}>☰</button>
      <ul id="nav-menu" className={menuActive ? 'active' : ''}>
        <li><a className="nav-link" href="index.html">Home</a></li>
        <li><a className="nav-link" href="about.html">About</a></li>
      </ul>
    </nav>
    <NavLink to="/" 
    className={({ isActive }) => isActive ? 'nav-link active' : 'nav-link'}>
    Home
  </NavLink>
  <NavLink to="/about" 
  className={({ isActive }) => isActive ? 'nav-link active' : 'nav-link'}>
    About
  </NavLink>
  </>
  );
}
export default Navbar;
