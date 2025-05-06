import ky from 'ky';

export const apiClient = () => {
    const api = ky.create({});
    const baseUrl = 'https://localhost:7257/api';

    function get_all_categories() {
        return api.get(`${baseUrl}/Category/getall`).json();
    }

    return {
        get_all_categories
    };
};

export default apiClient;