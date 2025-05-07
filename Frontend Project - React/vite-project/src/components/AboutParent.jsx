import React, { useState } from 'react';

function AboutParent() {
  const [aboutText, setAboutText] = useState(
    'Ethan Leo is a seasoned and results-driven Project Manager who brings experience and expertise to project management. With a proven track record of successfully delivering complex projects on time and within budget, Ethan Leo is the go-to professional for organizations seeking efficient and effective project leadership.'
  );
  const [isEditing, setIsEditing] = useState(true); // true لو عايزة تخلي التعديل مفتوح

  const handleChange = (e) => {
    setAboutText(e.target.value);
  };

  return (
    <div style={{ padding: '20px', textAlign:'center' }}>
      <h2>About Parent</h2>
      {isEditing ? (
        <textarea
          value={aboutText}
          onChange={handleChange}
          rows="6"
          cols="80"
        />
      ) : (
        <p>{aboutText}</p>
      )}
    </div>
  );
}

export default AboutParent;
