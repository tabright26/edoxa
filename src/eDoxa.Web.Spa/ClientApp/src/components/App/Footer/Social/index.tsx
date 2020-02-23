import React, { FunctionComponent } from "react";
import {
  Facebook,
  Twitter,
  Discord,
  Linkedin,
  Instagram
} from "components/Shared/Social";

type Props = {
  className?: string;
};

export const Social: FunctionComponent<Props> = ({ className = null }) => (
  <ul className={`m-0 p-0 ${className}`}>
    <li className="d-inline mr-3">
      <Facebook size="lg" />
    </li>
    <li className="d-inline mx-3">
      <Twitter size="lg" />
    </li>
    <li className="d-inline mx-3">
      <Discord size="lg" />
    </li>
    <li className="d-inline mx-3">
      <Linkedin size="lg" />
    </li>
    <li className="d-inline ml-3">
      <Instagram size="lg" />
    </li>
  </ul>
);
