import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

const Home = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const [isMobile, setIsMobile] = useState(window.innerWidth <= 768);

  // تتبع تغيير حجم الشاشة
  useEffect(() => {
    const handleResize = () => {
      setIsMobile(window.innerWidth <= 768);
      if (window.innerWidth > 768) {
        setIsMenuOpen(false); // إغلاق القائمة على الشاشات الكبيرة
      }
    };
    window.addEventListener('resize', handleResize);
    handleResize(); // تشغيل عند التحميل
    return () => window.removeEventListener('resize', handleResize);
  }, []);

  const styles = {
    home: {
      fontFamily: 'Arial, sans-serif',
    },
    navbar: {
      backgroundColor: '#3498db',
      padding: '10px 20px',
      color: 'white',
      borderBottom: '1px solid #ddd',
    },
    navContainer: {
      display: 'flex',
      justifyContent: 'space-between',
      alignItems: 'center',
      width: '100%',
    },
    navMenuHidden: {
      display: 'none',
      flexDirection: 'column',
      alignItems: 'flex-start',
    },
    navMenuActive: {
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'flex-start',
    },
    mobileMenuButton: {
      backgroundColor: 'white',
      color: '#3498db',
      border: 'none',
      fontSize: '1.5rem',
      cursor: 'pointer',
      padding: '5px 10px',
      display: 'block',
    },
    navMenu: {
      listStyle: 'none',
      padding: 0,
      margin: 0,
      gap: '15px',
    },
    navItem: {
      textDecoration: 'none',
      color: 'white',
      fontWeight: 'bold',
      display: 'block',
      padding: '5px 10px',
    },
    hero: {
      textAlign: 'center',
      padding: '100px 20px',
      backgroundColor: '#f0f8ff',
    },
    h1orders: {
      fontSize: '2.5rem',
      color: '#2c3e50',
    },
    p: {
      fontSize: '1.2rem',
      margin: '20px 0',
      color: '#555',
    },
    btn: {
      margin: '10px',
      padding: '12px 24px',
      fontSize: '1rem',
      borderRadius: '10px',
      backgroundColor: '#3498db',
      color: 'white',
      textDecoration: 'none',
      border: 'none',
      cursor: 'pointer',
    },
    btnOutline: {
      backgroundColor: 'white',
      color: '#3498db',
      border: '2px solid #3498db',
    },
    desktopNavMenu: {
      display: 'flex',
      flexDirection: 'row',
      alignItems: 'center',
      gap: '15px',
    },
    desktopMobileMenuButton: {
      display: 'none',
    },
  };

  return (
    <div style={styles.home} >
      <nav style={styles.navbar}>
        <div style={styles.navContainer}>
          <a href="/" style={{ ...styles.navItem, fontSize: '1.2rem' }}>
            📚 Ma'man
          </a>

          <button
            id="mobile-menu"
            onClick={() => setIsMenuOpen(!isMenuOpen)}
            style={isMobile ? styles.mobileMenuButton : styles.desktopMobileMenuButton}
          >
            ☰
          </button>

          <ul
            id="nav-menu"
            style={
              isMobile
                ? isMenuOpen
                  ? styles.navMenuActive
                  : styles.navMenuHidden
                : styles.desktopNavMenu
            }
          >
            <li><Link to="/" style={styles.navItem}>Home</Link></li>
            <li><Link to="/about" style={styles.navItem}>About</Link></li>
            <li><Link to="/contact" style={styles.navItem}>Contact</Link></li>
          </ul>
        </div>
      </nav>

      <div style={styles.hero} className="vh-100 d-flex flex-column justify-content-center align-items-center">
        <h1 style={styles.h1}>Welcome to Ma'man </h1>
        <p style={styles.p}>Online School for kids _Fun and easy lessons for young learners!_</p>
        <div>
          <Link to="/login" style={styles.btn}>Login</Link>
          <Link to="/register" style={{ ...styles.btn, ...styles.btnOutline }}>
            Register
          </Link>
        </div>
      </div>
    </div>
  );
};

export default Home;