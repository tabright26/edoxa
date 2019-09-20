import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faDollarSign } from "@fortawesome/free-solid-svg-icons";

const MoneyIcon = ({ ...attributes }) => <FontAwesomeIcon icon={faDollarSign} {...attributes} />;

export default MoneyIcon;
