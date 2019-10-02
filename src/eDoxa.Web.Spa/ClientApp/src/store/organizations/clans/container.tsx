import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadClans } from "store/organizations/clans/actions";
import { AppState } from "store/types";

export const connectClans = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, clans, ...attributes }) => {
    useEffect((): void => {
      actions.loadClans();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
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
        loadClans: () => dispatch(loadClans())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
