import React, { useState } from 'react';
import deskImage from '../assets/welcomeback.jpg';
import './login.css';

const Login =() => {
  const [formData, setFormData] = useState({
    email: '',
    password: '',
    rememberMe: false,
  });

  const [errors, setErrors] = useState({
    email: '',
    password: '',
  });

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    const newValue = type === 'checkbox' ? checked : value;
    setFormData({ ...formData, [name]: newValue });
    if (type !== 'checkbox') {
      validateField(name, value);
    }
  };

  const validateField = (name, value) => {
    let error = '';
    switch (name) {
      case 'email':
        if (!value) error = 'Email is required';
        else if (!/\S+@\S+\.\S+/.test(value)) error = 'Email is invalid';
        break;
      case 'password':
        if (!value) error = 'Password is required';
        else if (value.length < 6) error = 'Password must be at least 6 characters';
        break;
      default:
        break;
    }
    setErrors({ ...errors, [name]: error });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const hasErrors = Object.values(errors).some((error) => error !== '');
    if (!hasErrors) {
      console.log('Form submitted:', formData);
      alert('Login successful!');
    } else {
      console.log('Validation errors:', errors);
    }
  };

  const handleCreateAccount = () => {
    alert('Redirecting to create an account...');
  };

  return (
    <div className="login-container">
      <div className="image-section">
        <img src={deskImage} alt="Desk Illustration" />
      </div>
      <div className="form-section">
        <h2>Welcome Back</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Your Email</label>
            <div className="input-wrapper">
              <span className="icon">👤</span>
              <input
                type="email"
                name="email"
                value={formData.email}
                onChange={handleChange}
                className={errors.email ? 'error' : ''}
              />
            </div>
            {errors.email && <span className="error">{errors.email}</span>}
          </div>
          <div className="form-group">
            <label>Password</label>
            <div className="input-wrapper">
              <span className="icon">🔒</span>
              <input
                type="password"
                name="password"
                value={formData.password}
                onChange={handleChange}
                className={errors.password ? 'error' : ''}
              />
            </div>
            {errors.password && <span className="error">{errors.password}</span>}
          </div>
          <div className="form-group checkbox-group">
            <label>
              <input
                type="checkbox"
                name="rememberMe"
                checked={formData.rememberMe}
                onChange={handleChange}
              />
              Remember me
            </label>
          </div>
          <button type="submit">Log in</button>
        </form>
        <p className="create-account">
          <a href="#" onClick={handleCreateAccount}>Create an account</a>
        </p>
      </div>
    </div>
  );
}

export default Login;