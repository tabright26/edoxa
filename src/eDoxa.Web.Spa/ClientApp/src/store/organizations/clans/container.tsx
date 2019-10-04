import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadClans, loadClan, addClan } from "store/organizations/clans/actions";
import { AppState } from "store/types";

export const connectClans = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, clans, ...attributes }) => {
    return <ConnectedComponent actions={actions} clans={clans} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      clans: state.organizations.clans
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadClans: () => dispatch(loadClans()),
        loadClan: (clanId: string) => dispatch(loadClan(clanId)),
        addClan: (data: any) => dispatch(addClan(data))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
