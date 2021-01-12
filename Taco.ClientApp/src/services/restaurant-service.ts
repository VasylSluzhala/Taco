import { Restaurant } from '../models/restaurant';
import { RestaurantsFilter } from '../models/restaurants-filter';
import instanceApi from '../utils/instanceApi';

const getRestaurantsByFilter = async (filter: RestaurantsFilter): Promise<Restaurant[]> => {
    var response = await instanceApi.post<Restaurant[]>(`restaurant/getbyfilter`, filter);
    return response?.data;
}

export { getRestaurantsByFilter }