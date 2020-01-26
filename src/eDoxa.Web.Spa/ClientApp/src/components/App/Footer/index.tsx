import React from "react";
import { LinkContainer } from "react-router-bootstrap";

const Footer = () => (
  <div className="ml-auto">
    &copy; {new Date(Date.now()).getFullYear()} eDoxa - All rights reserved.
    <span className="mx-2">|</span>
    <LinkContainer to="/terms-of-services">
      <span>Terms of Services</span>
    </LinkContainer>
  </div>
);

export default Footer;
