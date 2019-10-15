import React, { FunctionComponent } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCcMastercard, faCcAmex, faCcStripe, faCcVisa, faCcDiscover } from "@fortawesome/free-brands-svg-icons";
import { CardBrandIconProps } from "./types";

const CardBrandIcon: FunctionComponent<CardBrandIconProps> = ({ brand, size }) => {
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
