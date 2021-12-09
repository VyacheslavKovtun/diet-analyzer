export interface Nutrient {
    name: string;
    amount: number;
    unit: string;
    percentOfDailyNeeds: number;
}

export interface NutritionSummary {
    nutrients: Nutrient[];
}

export interface NutritionSummaryBreakfast {
    nutrients: Nutrient[];
}

export interface NutritionSummaryLunch {
    nutrients: Nutrient[];
}

export interface NutritionSummaryDinner {
    nutrients: Nutrient[];
}

export interface Ingredient {
    name: string;
    unit: string;
    amount: string;
    image: string;
}

export interface Value {
    servings: number;
    id: number;
    title: string;
    imageType: string;
    image: string;
    ingredients: Ingredient[];
}

export interface Item {
    id: number;
    slot: number;
    position: number;
    type: string;
    value: Value;
}

export interface Day {
    nutritionSummary: NutritionSummary;
    nutritionSummaryBreakfast: NutritionSummaryBreakfast;
    nutritionSummaryLunch: NutritionSummaryLunch;
    nutritionSummaryDinner: NutritionSummaryDinner;
    date: number;
    day: string;
    items: Item[];
}

export interface WeekPlan {
    days: Day[];
}