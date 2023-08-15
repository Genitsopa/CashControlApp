export interface CompanyProduct {
  productId: number;
  productName: string;
  productQuantity: number;
  productRemainings: number;
  sellingPrice: number;
  profit: number;
  isEditing?: boolean; // Added property
}
