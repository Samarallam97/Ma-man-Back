export function CategoryCard({ name, description, id }) {
    return (
        <div>
            <h1>{id}</h1>
            <h2>{name}</h2>
            <h3>{description}</h3>
        </div>
    );
}

export default CategoryCard;