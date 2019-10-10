import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadCandidatures, loadCandidature, addCandidature, acceptCandidature, declineCandidature } from "store/organizations/candidatures/actions";
import { AppState } from "store/types";

interface CandidatureProps {
  type: string;
  id: string;
}

export const connectCandidatures = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, candidatures, ownProps, ...attributes }) => {
    useEffect(() => {
      switch (ownProps.type) {
        case "user":
          if (!candidatures.some(candidature => candidature.userId === ownProps.id)) {
            actions.loadCandidatures();
          }
          break;
        case "clan":
          actions.loadCandidatures();
          break;
        default:
          break;
      }

      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [candidatures]);
    return <ConnectedComponent actions={actions} candidatures={candidatures} {...attributes} />;
  };

  const mapStateToProps = (state: AppState, ownProps: CandidatureProps) => {
    const candidatures = state.organizations.candidatures.map(candidature => {
      const doxaTag = state.doxaTags.find(doxaTag => doxaTag.userId === candidature.userId);
      const clan = state.organizations.clans.find(clan => clan.id === candidature.clanId);

      candidature.userDoxaTag = doxaTag ? doxaTag.name + "#" + doxaTag.code : null;
      candidature.clanName = clan ? clan.name : null;
      return candidature;
    });

    return {
      candidatures,
      ownProps
    };
  };

  const mapDispatchToProps = (dispatch: any, ownProps: CandidatureProps) => {
    return {
      actions: {
        loadCandidatures: () => dispatch(loadCandidatures(ownProps.type, ownProps.id)),
        loadCandidature: (candidatureId: string) => dispatch(loadCandidature(candidatureId)),
        acceptCandidature: (candidatureId: string) => dispatch(acceptCandidature(candidatureId)).then(loadCandidatures(ownProps.type, ownProps.id)),
        declineCandidature: (candidatureId: string) => dispatch(declineCandidature(candidatureId)).then(loadCandidatures(ownProps.type, ownProps.id)),
        addCandidature: (clanId: string, userId: string) => dispatch(addCandidature(clanId, userId)).then(loadCandidatures(ownProps.type, ownProps.id))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
