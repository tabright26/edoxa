import React, { FunctionComponent } from "react";
import {
  getLegalTermsOfUsePath,
  getLegalPrivacyPolicyPath
} from "utils/coreui/constants";
import { Link } from "react-router-dom";

interface Props {
  className?: string;
}

const Footer: FunctionComponent<Props> = ({ className = null }) => (
  <div className={className}>
    &copy; {new Date(Date.now()).getFullYear()} eDoxa - All rights reserved.
    <span className="mx-2">|</span>
    <Link to={getLegalTermsOfUsePath()}>Terms of Use</Link>
    <span className="mx-2">|</span>
    <Link to={getLegalPrivacyPolicyPath()}>Privacy Policy</Link>
  </div>
);

export default Footer;
