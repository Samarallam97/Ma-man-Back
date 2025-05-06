import React, { useState } from 'react';
import familyImage from '../assets/family.jpg';
import './register.css';

const Register= () =>{
  const [formData, setFormData] = useState({
    firstName: '',
    secondName: '',
    email: '',
    password: '',
    country: '',
    phoneNumber: '',
    nationalId: '',
    photo: null,
  });

  const [errors, setErrors] = useState({
    firstName: '',
    secondName: '',
    email: '',
    password: '',
    country: '',
    phoneNumber: '',
    nationalId: '',
    photo: '',
  });

  const [previewImage, setPreviewImage] = useState(null);

  const handleChange = (e) => {
    const { name, value, files } = e.target;
    if (name === 'photo' && files && files[0]) {
      const file = files[0];
      setFormData({ ...formData, [name]: file });
      validateField(name, file);
      const reader = new FileReader();
      reader.onloadend = () => {
        setPreviewImage(reader.result);
      };
      reader.readAsDataURL(file);
    } else {
      setFormData({ ...formData, [name]: value });
      validateField(name, value);
    }
  };

  const validateField = (name, value) => {
    let error = '';
    switch (name) {
      case 'firstName':
        if (!value) error = 'First Name is required';
        else if (value.length < 2) error = 'First Name must be at least 2 characters';
        break;
      case 'secondName':
        if (!value) error = 'Second Name is required';
        else if (value.length < 2) error = 'Second Name must be at least 2 characters';
        break;
      case 'email':
        if (!value) error = 'Email is required';
        else if (!/\S+@\S+\.\S+/.test(value)) error = 'Email is invalid';
        break;
      case 'password':
        if (!value) error = 'Password is required';
        else if (value.length < 6) error = 'Password must be at least 6 characters';
        break;
      case 'country':
        if (!value) error = 'Country is required';
        break;
      case 'phoneNumber':
        if (!value) error = 'Phone Number is required';
        else if (!/^\d{10,15}$/.test(value)) error = 'Phone Number must be 10-15 digits';
        break;
      case 'nationalId':
        if (!value) error = 'National ID is required';
        else if (!/^\d{8,12}$/.test(value)) error = 'National ID must be 8-12 digits';
        break;
      case 'photo':
        if (!value) error = 'Photo is required';
        else if (!value.type.startsWith('image/')) error = 'File must be an image';
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
      console.log('Form submitted:', {
        ...formData,
        photo: formData.photo ? formData.photo.name : null,
      });
      alert('Registration successful!');
    } else {
      console.log('Validation errors:', errors);
    }
  };

  return (
    <div className="register-container">
      <div className="image-section" style={{ display: 'flex', alignItems: 'center', height: '100%' }}>
        <img src={familyImage} alt="Family Illustration" />
      </div>
      <div className="form-section">
        <h2>REGISTRATION FORM</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-row">
            <div className="form-group">
              <label>First Name</label>
              <input
                type="text"
                name="firstName"
                value={formData.firstName}
                onChange={handleChange}
                className={errors.firstName ? 'error' : ''}
              />
              {errors.firstName && <span className="error">{errors.firstName}</span>}
            </div>
            <div className="form-group">
              <label>Second Name</label>
              <input
                type="text"
                name="secondName"
                value={formData.secondName}
                onChange={handleChange}
                className={errors.secondName ? 'error' : ''}
              />
              {errors.secondName && <span className="error">{errors.secondName}</span>}
            </div>
          </div>
          <div className="form-row">
            <div className="form-group">
              <label>Email</label>
              <input
                type="email"
                name="email"
                value={formData.email}
                onChange={handleChange}
                className={errors.email ? 'error' : ''}
              />
              {errors.email && <span className="error">{errors.email}</span>}
            </div>
          </div>
          <div className="form-row">
            <div className="form-group">
              <label>Password</label>
              <input
                type="password"
                name="password"
                value={formData.password}
                onChange={handleChange}
                className={errors.password ? 'error' : ''}
              />
              {errors.password && <span className="error">{errors.password}</span>}
            </div>
          </div>
          <div className="form-row">
            <div className="form-group">
              <label>Country</label>
              <select
                name="country"
                value={formData.country}
                onChange={handleChange}
                className={errors.country ? 'error' : ''}
              >
                <option value="">Select Country</option>
                <option value="Egypt">Egypt</option>
                <option value="USA">USA</option>
                <option value="UK">UK</option>
              </select>
              {errors.country && <span className="error">{errors.country}</span>}
            </div>
          </div>
          <div className="form-row">
            <div className="form-group">
              <label>Phone Number</label>
              <input
                type="text"
                name="phoneNumber"
                value={formData.phoneNumber}
                onChange={handleChange}
                className={errors.phoneNumber ? 'error' : ''}
              />
              {errors.phoneNumber && <span className="error">{errors.phoneNumber}</span>}
            </div>
          </div>
          <div className="form-row">
            <div className="form-group">
              <label>National ID</label>
              <input
                type="text"
                name="nationalId"
                value={formData.nationalId}
                onChange={handleChange}
                className={errors.nationalId ? 'error' : ''}
              />
              {errors.nationalId && <span className="error">{errors.nationalId}</span>}
            </div>
          </div>
          <div className="form-row">
            <div className="form-group">
              <label>Photo</label>
              <input
                type="file"
                name="photo"
                accept="image/*"
                onChange={handleChange}
                className={errors.photo ? 'error' : ''}
              />
              {previewImage && <img src={previewImage} alt="Preview" className="preview-image" />}
              {errors.photo && <span className="error">{errors.photo}</span>}
            </div>
          </div>
          <button type="submit">Register</button>
        </form>
      </div>
    </div>
  );
}

export default Register;