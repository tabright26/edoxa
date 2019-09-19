import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { SizeProp } from "@fortawesome/fontawesome-svg-core";
import { faCcMastercard, faCcAmex, faCcStripe, faCcVisa, faCcDiscover } from "@fortawesome/free-brands-svg-icons";

type BrandProp = "visa" | "amex" | "mastercard" | "discover";

interface CardBrandIconProps {
  brand: BrandProp;
  size?: SizeProp;
}

const CardBrandIcon = ({ brand, size }: CardBrandIconProps) => {
  if (brand === "visa") {
    return <FontAwesomeIcon size={size} icon={faCcVisa} />;
  }
  if (brand === "amex") {
    return <FontAwesomeIcon size={size} icon={faCcAmex} />;
  }
  if (brand === "mastercard") {
    return <FontAwesomeIcon size={size} icon={faCcMastercard} />;
  }
  if (brand === "discover") {
    return <FontAwesomeIcon size={size} icon={faCcDiscover} />;
  }
  return <FontAwesomeIcon size={size} icon={faCcStripe} />;
};

export default CardBrandIcon;
