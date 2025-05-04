import React from 'react';
import './home.css';

import { useEffect } from 'react';


const Home = () => { useEffect(() => {
    const mobileMenu = document.getElementById('mobile-menu');
    const navMenu = document.getElementById('nav-menu');
  
    mobileMenu?.addEventListener('click', () => {
      navMenu?.classList.toggle('active');
    });
 
  let slideIndex = 1;

  const showSlides = (n) => {
    const slides = document.getElementsByClassName("testimonial");
    const dots = document.getElementsByClassName("dot");

    if (n > slides.length) slideIndex = 1;
    if (n < 1) slideIndex = slides.length;

    for (let i = 0; i < slides.length; i++) {
      slides[i].classList.remove("active");
    }

    for (let i = 0; i < dots.length; i++) {
      dots[i].classList.remove("active");
    }

    slides[slideIndex - 1]?.classList.add("active");
    dots[slideIndex - 1]?.classList.add("active");
  };

  const currentSlide = (n) => {
    slideIndex = n;
    showSlides(slideIndex);
  };

  window.currentSlide = currentSlide; // علشان نقدر نستخدمها في JSX

  showSlides(slideIndex);
  const interval = setInterval(() => {
    slideIndex++;
    showSlides(slideIndex);
  }, 5000);
    return () => clearInterval(interval); // تنظيف
     }, []);

  return (
    <div className='home'>
   
    <nav className="navbar">
        <div className="nav-container">
            <a href="index.html" className="logo">
                <i className="fas fa-book-open"></i> Knowledge Hub
            </a>

            <div className="menu-toggle" id="mobile-menu">
                <i className="fas fa-bars"></i>
            </div>

            <ul className="nav-menu" id="nav-menu">
                <li className="nav-item">
                    <a href="index.html" className="nav-link">
                        <i className="fas fa-home"></i> Home
                    </a>
                </li>
                <li className="nav-item">
                    <div className="search-bar">
                        <input type="text" className="search-input" placeholder="Search..."/>
                        <button className="search-btn">
                            <i className="fas fa-search"></i>
                        </button>
                    </div>
                </li>
                <li className="nav-item">
                    <a href="culture.html" className="nav-link">
                        <i className="fas fa-globe"></i> Culture &amp; Society
                    </a>
                </li>
                <li className="nav-item">
                    <a href="digital-skills.html" className="nav-link">
                        <i className="fas fa-laptop-code"></i> Digital Skills
                    </a>
                </li>
                <li className="nav-item">
                    <a href="books.html" className="nav-link">
                        <i className="fas fa-book"></i> Books
                    </a>
                </li>
                <li className="nav-item">
                    <a href="stories.html" className="nav-link">
                        <i className="fas fa-book-open"></i> Stories
                    </a>
                </li>
                <li className="nav-item">
                    <a href="health.html" className="nav-link">
                        <i className="fas fa-heartbeat"></i> Health &amp; Sports
                    </a>
                </li>
                <li className="nav-item">
                    <a href="intelligence.html" className="nav-link">
                        <i className="fas fa-brain"></i> Intelligence
                    </a>
                </li>
                <li className="nav-item">
                    <a href="entertainment.html" className="nav-link">
                        <i className="fas fa-film"></i> Entertainment
                    </a>
                </li>
                <li className="nav-item">
                    <a href="self-development.html" className="nav-link">
                        <i className="fas fa-user-graduate"></i> Self-Development
                    </a>
                </li>
                <li className="nav-item">
                    <a href="islamic-studies.html" className="nav-link">
                        <i className="fas fa-mosque"></i> Islamic Studies
                    </a>
                </li>
            </ul>
        </div>
    </nav>

    <div className="container">
        <section className="hero">
            <h1>Expand Your Knowledge Horizon</h1>
            <p>Discover a world of learning with our comprehensive resources on culture, technology, health, and personal development.</p>
            <a href="#features" className="btn">Explore Features</a>
            <a href="#categories" className="btn btn-outline">Browse Categories</a>
        </section>

        <section id="features">
            <div className="section-title">
                <h2>Why Choose Knowledge Hub</h2>
            </div>
            <div className="features">
                <div className="feature-card">
                    <div className="feature-icon">
                        <i className="fas fa-graduation-cap"></i>
                    </div>
                    <h3>Expert Content</h3>
                    <p>Curated by professionals and scholars to ensure accurate and reliable information across all subjects.</p>
                </div>
                <div className="feature-card">
                    <div className="feature-icon">
                        <i className="fas fa-layer-group"></i>
                    </div>
                    <h3>Diverse Topics</h3>
                    <p>From technology to spirituality, we cover a wide range of subjects to satisfy every curious mind.</p>
                </div>
                <div className="feature-card">
                    <div className="feature-icon">
                        <i className="fas fa-user-friends"></i>
                    </div>
                    <h3>Community Driven</h3>
                    <p>Join discussions, ask questions, and share knowledge with our growing community of learners.</p>
                </div>
            </div>
        </section>

        <section id="categories" className="categories">
            <div className="section-title">
                <h2>Explore Our Categories</h2>
            </div>
            <div className="category-grid">
                <div className="category-card">
                    <img src="https://images.unsplash.com/photo-1454165804606-c3d57bc86b40?ixlib=rb-1.2.1&amp;auto=format&amp;fit=crop&amp;w=1350&amp;q=80" alt="Digital Skills" className="category-img"/>
                    <div className="category-overlay">
                        <h3>Digital Skills</h3>
                        <p>Master the tools of the digital age</p>
                    </div>
                </div>
                <div className="category-card">
                    <img src="https://images.unsplash.com/photo-1498837167922-ddd27525d352?ixlib=rb-1.2.1&amp;auto=format&amp;fit=crop&amp;w=1350&amp;q=80" alt="Health &amp; Sports" className="category-img"/>
                    <div className="category-overlay">
                        <h3>Health &amp; Sports</h3>
                        <p>Wellness for body and mind</p>
                    </div>
                </div>
                <div className="category-card">
                    <img src="https://images.unsplash.com/photo-1507842217343-583bb7270b66?ixlib=rb-1.2.1&amp;auto=format&amp;fit=crop&amp;w=1350&amp;q=80" alt="Books" className="category-img"/>
                    <div className="category-overlay">
                        <h3>Books</h3>
                        <p>Timeless knowledge in written form</p>
                    </div>
                </div>
                <div className="category-card">
                    <img src="https://images.unsplash.com/photo-1495446815901-a7297e633e8d?ixlib=rb-1.2.1&amp;auto=format&amp;fit=crop&amp;w=1350&amp;q=80" alt="Stories" className="category-img"/>
                    <div className="category-overlay">
                        <h3>Stories</h3>
                        <p>Narratives that inspire and teach</p>
                    </div>
                </div>
            </div>
        </section>

        <section className="testimonials">
            <div className="section-title">
                <h2>What Our Users Say</h2>
            </div>
            <div className="testimonial-slider">
                <div className="testimonial">
                    <p className="testimonial-text">"Knowledge Hub has transformed my learning journey. The diverse content and easy navigation make it my go-to resource for everything from tech skills to personal development."</p>
                    <p className="testimonial-author">- Sarah Johnson, Educator</p>
                </div>
                <div className="testimonial active">
                    <p className="testimonial-text">"As a lifelong learner, I appreciate the depth and quality of articles. The Islamic Studies section is particularly well-researched and presented."</p>
                    <p className="testimonial-author">- Ahmed Mahmoud, Student</p>
                </div>
                <div className="testimonial">
                    <p className="testimonial-text">"The digital skills tutorials helped me transition careers. The step-by-step guides are perfect for beginners and advanced learners alike."</p>
                    <p className="testimonial-author">- Michael Chen, Web Developer</p>
                </div>
                <div className="testimonial-dots">
                    <span className="dot" onClick={()=>window.currentSlide(1)}></span>
                    <span className="dot active" onClick={()=>window.currentSlide(2)}></span>
                    <span className="dot" onClick={()=>window.currentSlide(3)}></span>
                </div>
            </div>
        </section>

        <section className="newsletter">
            <h2>Stay Updated</h2>
            <p>Subscribe to our newsletter and receive weekly updates on new content, features, and learning resources.</p>
            <form className="newsletter-form">
                <input type="email" className="newsletter-input" placeholder="Your email address" required=""/>
                <button type="submit" className="newsletter-btn">Subscribe</button>
            </form>
        </section>
    </div>

    <footer>
        <div className="footer-content">
            <div className="footer-column">
                <h3>About Us</h3>
                <p>Knowledge Hub is dedicated to providing high-quality educational content across various disciplines to foster lifelong learning.</p>
                <div className="social-links">
                    <a href="#"><i className="fab fa-facebook-f"></i></a>
                    <a href="#"><i className="fab fa-twitter"></i></a>
                    <a href="#"><i className="fab fa-instagram"></i></a>
                    <a href="#"><i className="fab fa-linkedin-in"></i></a>
                </div>
            </div>
            <div className="footer-column">
                <h3>Quick Links</h3>
                <ul className="footer-links">
                    <li><a href="index.html">Home</a></li>
                    <li><a href="culture.html">Culture &amp; Society</a></li>
                    <li><a href="digital-skills.html">Digital Skills</a></li>
                    <li><a href="books.html">Books</a></li>
                    <li><a href="stories.html">Stories</a></li>
                </ul>
            </div>
            <div className="footer-column">
                <h3>Resources</h3>
                <ul className="footer-links">
                    <li><a href="health.html">Health &amp; Sports</a></li>
                    <li><a href="intelligence.html">Intelligence</a></li>
                    <li><a href="entertainment.html">Entertainment</a></li>
                    <li><a href="self-development.html">Self-Development</a></li>
                    <li><a href="islamic-studies.html">Islamic Studies</a></li>
                </ul>
            </div>
            <div className="footer-column">
                <h3>Contact</h3>
                <p><i className="fas fa-envelope"></i> info@knowledgehub.com</p>
                <p><i className="fas fa-phone"></i> +1 (555) 123-4567</p>
                <p><i className="fas fa-map-marker-alt"></i> 123 Learning St, Education City</p>
            </div>
        </div>
        <div className="copyright">
            <p>© 2023 Knowledge Hub. All rights reserved.</p>
        </div>
    </footer>

   
    </div>
  )
}

export default Home;