import { useEffect, useState } from 'react';
import React from 'react';
function TestimonialSlider() {
  const [slideIndex, setSlideIndex] = useState(1);

  useEffect(() => {
    const interval = setInterval(() => {
      setSlideIndex(prev => (prev >= 3 ? 1 : prev + 1));
    }, 5000);
    return () => clearInterval(interval);
  }, []);

  const testimonials = [
    "Testimonial 1",
    "Testimonial 2",
    "Testimonial 3",
  ];

  return (
    <div className="testimonial-slider">
      {testimonials.map((text, index) => (
        <div
          key={index}
          className={`testimonial ${slideIndex === index + 1 ? 'active' : ''}`}
        >
          {text}
        </div>
      ))}

      <div className="dots">
        {[1, 2, 3].map(n => (
          <span
            key={n}
            className={`dot ${slideIndex === n ? 'active' : ''}`}
            onClick={() => setSlideIndex(n)}
          ></span>
        ))}
      </div>
    </div>
  );
}
export default TestimonialSlider;