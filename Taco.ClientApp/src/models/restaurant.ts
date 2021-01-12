import { Category } from "./category";

export interface Restaurant {
    id: number;
    name: string;
    city: string;
    suburb: string;
    logoPath: string;
    rank: number;
    categories: Category[];
}