export interface Ingredient {
    description: string;
    name: string;
    safety_level: string;
}

export interface Nutrient {
    name: string;
    amount: number;
    unit: string;
    percentOfDailyNeeds: number;
}

export interface CaloricBreakdown {
    percentProtein: number;
    percentFat: number;
    percentCarbs: number;
}

export interface Nutrition {
    nutrients: Nutrient[];
    caloricBreakdown: CaloricBreakdown;
}

export interface Servings {
    number: number;
    size: number;
    unit: string;
}

export interface ProductInfo {
    id: number;
    title: string;
    breadcrumbs: string[];
    imageType: string;
    badges: string[];
    importantBadges: string[];
    ingredientCount: number;
    generatedText?: any;
    ingredientList: string;
    ingredients: Ingredient[];
    likes: number;
    aisle: string;
    nutrition: Nutrition;
    price: number;
    servings: Servings;
    spoonacularScore: number;
}