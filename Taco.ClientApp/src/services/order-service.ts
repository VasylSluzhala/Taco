import instanceApi from '../utils/instanceApi';

const createOrder = async (menuItemIds: number[]): Promise<void> => {
    return await instanceApi.post(`order/addorder`, menuItemIds);
}

export { createOrder }