import React, { Fragment, FunctionComponent } from "react";
import { LinkContainer } from "react-router-bootstrap";
import {
  AppNavbarBrand,
  AppSidebarToggler,
  AppAsideToggler
} from "@coreui/react";
import AppDropdown from "components/App/Dropdown";
import logo from "assets/img/brand/logo.png";
import sygnet from "assets/img/brand/sygnet.png";
import { getDefaultPath } from "utils/coreui/constants";
import AppNav from "components/App/Nav";

const Header: FunctionComponent = () => {
  return (
    <Fragment>
      <AppSidebarToggler className="d-lg-none" display="md" mobile />
      <LinkContainer to={getDefaultPath()}>
        <AppNavbarBrand
          full={{ src: logo, width: 85, height: 30, alt: "eDoxa Logo" }}
          minimized={{ src: sygnet, width: 30, height: 30, alt: "eDoxa Logo" }}
        />
      </LinkContainer>
      <AppSidebarToggler className="d-md-down-none" display="lg" hidden />
      <AppNav />
      <AppDropdown />
      <AppAsideToggler className="d-md-down-none" hidden />
    </Fragment>
  );
};

export default Header;
