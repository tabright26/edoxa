import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadLogo, updateLogo } from "store/organizations/logos/actions";
import { AppState } from "store/types";

export const connectLogo = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, clans, ...attributes }) => {
    return <ConnectedComponent actions={actions} clans={clans} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      logo: state.organizations.clans
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
