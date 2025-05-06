import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import CategoryCard from '../components/CategoryCard.jsx';
import apiClient from '../API/caategories.js';

const CategoriesPage = () => {
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        apiClient().get_all_categories().then((result) => {
            setCategories(result);
        });
    }, []);

    if (categories.length === 0) {
        return <div>Loading......</div>;
    }

    return (
        <div>
            {categories.map((category) => (
                <CategoryCard
                    name={category.name}
                    description={category.description}
                    id={category.id}
                />
            ))}
        </div>
    );
};

export default CategoriesPage;
