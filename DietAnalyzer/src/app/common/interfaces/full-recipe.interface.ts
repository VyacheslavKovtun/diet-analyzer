export interface Us {
    amount: number;
    unitShort: string;
    unitLong: string;
}

export interface Metric {
    amount: number;
    unitShort: string;
    unitLong: string;
}

export interface Measures {
    us: Us;
    metric: Metric;
}

export interface ExtendedIngredient {
    id: number;
    aisle: string;
    image: string;
    consistency: string;
    name: string;
    nameClean: string;
    original: string;
    originalString: string;
    originalName: string;
    amount: number;
    unit: string;
    meta: string[];
    metaInformation: string[];
    measures: Measures;
}

export interface Ingredient {
    id: number;
    name: string;
    localizedName: string;
    image: string;
}

export interface Equipment {
    id: number;
    name: string;
    localizedName: string;
    image: string;
}

export interface Length {
    number: number;
    unit: string;
}

export interface Step {
    number: number;
    step: string;
    ingredients: Ingredient[];
    equipment: Equipment[];
    length: Length;
}

export interface AnalyzedInstruction {
    name: string;
    steps: Step[];
}

export interface FullRecipe {
    vegetarian: boolean;
    vegan: boolean;
    glutenFree: boolean;
    dairyFree: boolean;
    veryHealthy: boolean;
    cheap: boolean;
    veryPopular: boolean;
    sustainable: boolean;
    weightWatcherSmartPoints: number;
    gaps: string;
    lowFodmap: boolean;
    preparationMinutes: number;
    cookingMinutes: number;
    aggregateLikes: number;
    spoonacularScore: number;
    healthScore: number;
    creditsText: string;
    sourceName: string;
    pricePerServing: number;
    extendedIngredients: ExtendedIngredient[];
    id: number;
    title: string;
    readyInMinutes: number;
    servings: number;
    sourceUrl: string;
    image: string;
    imageType: string;
    summary: string;
    cuisines: any[];
    dishTypes: string[];
    diets: string[];
    occasions: any[];
    instructions: string;
    analyzedInstructions: AnalyzedInstruction[];
    originalId?: any;
    spoonacularSourceUrl: string;
    license: string;
}