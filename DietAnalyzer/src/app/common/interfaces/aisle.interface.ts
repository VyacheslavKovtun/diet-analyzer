export interface Original {
    amount: number;
    unit: string;
}

export interface Metric {
    amount: number;
    unit: string;
}

export interface Us {
    amount: number;
    unit: string;
}

export interface Measures {
    original: Original;
    metric: Metric;
    us: Us;
}

export interface Item {
    id: number;
    name: string;
    measures: Measures;
    pantryItem: boolean;
    aisle: string;
    cost: number;
    ingredientId: number;
}

export interface Aisle {
    aisle: string;
    items: Item[];
}