import React, { FunctionComponent } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCcMastercard,
  faCcAmex,
  faCcStripe,
  faCcVisa,
  faCcDiscover
} from "@fortawesome/free-brands-svg-icons";
import { SizeProp } from "@fortawesome/fontawesome-svg-core";

interface Props {
  className?: string;
  size?: SizeProp;
  brand: BrandProp;
}

export type BrandProp = "visa" | "amex" | "mastercard" | "discover";

export const Icon: FunctionComponent<Props> = ({
  className = null,
  size = null,
  brand
}) => {
  if (brand === "visa") {
    return (
      <FontAwesomeIcon className={className} size={size} icon={faCcVisa} />
    );
  }
  if (brand === "amex") {
    return (
      <FontAwesomeIcon className={className} size={size} icon={faCcAmex} />
    );
  }
  if (brand === "mastercard") {
    return (
      <FontAwesomeIcon
        className={className}
        size={size}
        icon={faCcMastercard}
      />
    );
  }
  if (brand === "discover") {
    return (
      <FontAwesomeIcon className={className} size={size} icon={faCcDiscover} />
    );
  }
  return (
    <FontAwesomeIcon className={className} size={size} icon={faCcStripe} />
  );
};
