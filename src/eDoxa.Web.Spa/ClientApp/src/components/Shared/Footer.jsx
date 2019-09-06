import React from "react";
import { Link } from "react-router-dom";

const Footer = () => (
  <div className="ml-auto">
    &copy; {new Date(Date.now()).getFullYear()} eDoxa - All rights reserved.
    <span className="mx-2">|</span>
    <Link to="/terms-of-services">Terms of Services</Link>
  </div>
);

export default Footer;
