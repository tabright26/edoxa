import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadClan } from "store/organizations/clan/actions";
import { AppState } from "store/types";

export const connectClan = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, clan, ...attributes }) => {
    useEffect((): void => {
      actions.loadClan();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} clan={clan} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      clan: state.organizations.clan
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadClan: () => dispatch(loadClan())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
