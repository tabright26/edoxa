import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadLogo, updateLogo } from "store/organizations/logos/actions";
import { AppState } from "store/types";

export const connectLogo = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, logo, ...attributes }) => {
    return <ConnectedComponent actions={actions} logo={logo} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      logo: state.organizations.logos
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadLogo: (clanId: string) => dispatch(loadLogo(clanId)),
        updateLogo: (clanId: string, data: any) => dispatch(updateLogo(clanId, data))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
