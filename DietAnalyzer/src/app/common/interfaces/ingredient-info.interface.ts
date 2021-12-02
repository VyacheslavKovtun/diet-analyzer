export interface EstimatedCost {
    value: number;
    unit: string;
}

export interface Nutrient {
    name: string;
    amount: number;
    unit: string;
    percentOfDailyNeeds: number;
}

export interface Property {
    name: string;
    amount: number;
    unit: string;
}

export interface CaloricBreakdown {
    percentProtein: number;
    percentFat: number;
    percentCarbs: number;
}

export interface WeightPerServing {
    amount: number;
    unit: string;
}

export interface Nutrition {
    nutrients: Nutrient[];
    properties: Property[];
    caloricBreakdown: CaloricBreakdown;
    weightPerServing: WeightPerServing;
}

export interface IngredientInfo {
    id: number;
    original: string;
    originalName: string;
    name: string;
    nameClean: string;
    amount: number;
    unit: string;
    unitShort: string;
    unitLong: string;
    possibleUnits: string[];
    estimatedCost: EstimatedCost;
    consistency: string;
    shoppingListUnits: string[];
    aisle: string;
    image: string;
    meta: any[];
    nutrition: Nutrition;
    categoryPath: string[];
}