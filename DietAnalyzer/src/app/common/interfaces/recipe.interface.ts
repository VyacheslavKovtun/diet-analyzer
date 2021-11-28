export interface Recipe {
    id: number,
    title: string,
    image: string,
    servings: number,
    readyInMinutes: number,
    sourceUrl: string,
    aggregateLikes: number,
    dishTypes: string[]
}