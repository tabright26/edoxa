import { SizeProp } from "@fortawesome/fontawesome-svg-core";

export type BrandProp = "visa" | "amex" | "mastercard" | "discover";

export interface CardBrandIconProps {
  className: string;
  brand: BrandProp;
  size?: SizeProp;
}
