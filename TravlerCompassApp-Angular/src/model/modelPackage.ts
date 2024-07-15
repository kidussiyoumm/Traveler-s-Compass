import { IPackage } from "../Interfaces/IPackage.interface";

export class Modelpackage implements IPackage {
    Id: number;
    Tour: string;
    Price: number | null;
    Description: string | null;
    Title: string;
    Image: string | null;
  
    constructor(
      id: number = 0,
      tour: string = '',
      price: number | null = null,
      description: string | null = '',
      title: string = '',
      image: string | null = ''
    ) {
      this.Id = id;
      this.Tour = tour;
      this.Price = price;
      this.Description = description;
      this.Title = title;
      this.Image = image;
    }
  }