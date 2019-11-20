import React, { Fragment, FunctionComponent } from "react";
import { LinkContainer } from "react-router-bootstrap";
import {
  AppNavbarBrand,
  AppSidebarToggler,
  AppAsideToggler
} from "@coreui/react";
import { LoginMenu } from "utils/oidc/LoginMenu";
import logo from "assets/img/brand/logo.png";
import sygnet from "assets/img/brand/sygnet.png";

const Header: FunctionComponent = () => {
  return (
    <Fragment>
      <AppSidebarToggler className="d-lg-none" display="md" mobile />
      <LinkContainer to="/">
        <AppNavbarBrand
          full={{ src: logo, width: 85, height: 30, alt: "eDoxa Logo" }}
          minimized={{ src: sygnet, width: 30, height: 30, alt: "eDoxa Logo" }}
        />
      </LinkContainer>
      <AppSidebarToggler className="d-md-down-none" display="lg" hidden />
      {/* <Nav className="d-md-down-none" navbar>
        <NavItem className="px-3">
          <LinkContainer to="/marketplace">
            <NavLink className="nav-link">Marketplace</NavLink>
          </LinkContainer>
        </NavItem>
        <NavItem className="px-3">
          <LinkContainer to="/news-feeds">
            <NavLink className="nav-link">News Feeds</NavLink>
          </LinkContainer>
        </NavItem>
      </Nav> */}
      <LoginMenu />
      <AppAsideToggler className="d-md-down-none" hidden />
    </Fragment>
  );
};

export default Header;
