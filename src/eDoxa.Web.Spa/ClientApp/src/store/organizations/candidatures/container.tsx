import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadCandidaturesWithClanId, loadCandidaturesWithUserId, loadCandidature, addCandidature, acceptCandidature, declineCandidature } from "store/organizations/candidatures/actions";
import { AppState } from "store/types";

export const connectCandidatures = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, candidatures, ...attributes }) => {
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
        loadCandidaturesWithClanId: (clanId: string) => dispatch(loadCandidaturesWithClanId(clanId)),
        loadCandidaturesWithUserId: (userId: string) => dispatch(loadCandidaturesWithUserId(userId)),
        loadCandidature: (candidatureId: string) => dispatch(loadCandidature(candidatureId)),
        acceptCandidature: (candidatureId: string) => dispatch(acceptCandidature(candidatureId)),
        declineCandidature: (candidatureId: string) => dispatch(declineCandidature(candidatureId)),
        addCandidature: (data: any) => dispatch(addCandidature(data))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
