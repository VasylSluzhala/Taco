import axios from 'axios';

const instance = axios.create({
    baseURL: `${process.env.REACT_APP_API_URL}/api/`,
});

instance.interceptors.request.use((config) => {
    const newConfig = config;
    if (typeof window !== 'undefined') {
        newConfig.headers = {
            'Content-Type': 'application/json'
        };
    }

    return newConfig;
});

export default instance;