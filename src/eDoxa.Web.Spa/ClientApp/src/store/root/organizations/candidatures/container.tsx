import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadClanCandidatures, loadClanCandidature, sendClanCandidature, acceptClanCandidature, declineClanCandidature } from "store/root/organizations/candidatures/actions";
import { RootState } from "store/root/types";

interface CandidatureProps {
  type: string;
  id: string;
}

export const withCandidatures = (HighOrderComponent: FunctionComponent<any>) => {
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
    }, []);
    return <HighOrderComponent actions={actions} candidatures={candidatures} {...attributes} />;
  };

  const mapStateToProps = (state: RootState, ownProps: CandidatureProps) => {
    const candidatures = state.organizations.candidatures.data.map(candidature => {
      const doxaTag = state.doxatags.data.find(doxaTag => doxaTag.userId === candidature.userId);
      const clan = state.organizations.clans.data.find(clan => clan.id === candidature.clanId);

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
        loadCandidatures: () => dispatch(loadClanCandidatures(ownProps.type, ownProps.id)),
        loadCandidature: (candidatureId: string) => dispatch(loadClanCandidature(candidatureId)),
        acceptCandidature: (candidatureId: string) => dispatch(acceptClanCandidature(candidatureId)).then(loadClanCandidatures(ownProps.type, ownProps.id)),
        declineCandidature: (candidatureId: string) => dispatch(declineClanCandidature(candidatureId)).then(loadClanCandidatures(ownProps.type, ownProps.id)),
        addCandidature: (clanId: string, userId: string) => dispatch(sendClanCandidature(clanId, userId)).then(loadClanCandidatures(ownProps.type, ownProps.id))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
