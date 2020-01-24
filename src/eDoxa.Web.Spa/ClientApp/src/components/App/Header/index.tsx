import React, { Fragment, FunctionComponent } from "react";
import { LinkContainer } from "react-router-bootstrap";
import {
  AppNavbarBrand,
  AppSidebarToggler,
  AppAsideToggler
} from "@coreui/react";
import LoginMenu from "utils/oidc/LoginMenu";
import logo from "assets/img/brand/logo.png";
import sygnet from "assets/img/brand/sygnet.png";
import { getHomePath } from "utils/coreui/constants";
import AppNav from "components/App/Nav";

const Header: FunctionComponent = () => {
  return (
    <Fragment>
      <AppSidebarToggler className="d-lg-none" display="md" mobile />
      <LinkContainer to={getHomePath()}>
        <AppNavbarBrand
          full={{ src: logo, width: 85, height: 30, alt: "eDoxa Logo" }}
          minimized={{ src: sygnet, width: 30, height: 30, alt: "eDoxa Logo" }}
        />
      </LinkContainer>
      <AppSidebarToggler className="d-md-down-none" display="lg" hidden />
      <AppNav />
      <LoginMenu />
      <AppAsideToggler className="d-md-down-none" hidden />
    </Fragment>
  );
};

export default Header;