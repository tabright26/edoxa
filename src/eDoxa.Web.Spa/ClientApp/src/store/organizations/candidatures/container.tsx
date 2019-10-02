import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadCandidatures } from "store/organizations/candidatures/actions";
import { AppState } from "store/types";

export const connectCandidatures = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, candidatures, ...attributes }) => {
    useEffect((): void => {
      actions.loadCandidatures();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} candidatures={candidatures} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      candidatures: state.organizations.candidatures
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadCandidatures: () => dispatch(loadCandidatures())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
