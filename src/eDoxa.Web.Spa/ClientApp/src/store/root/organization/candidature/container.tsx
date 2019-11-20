import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import {
  loadClanCandidatures,
  loadClanCandidature,
  sendClanCandidature,
  acceptClanCandidature,
  declineClanCandidature
} from "store/root/organization/candidature/actions";
import { ClanCandidaturesState } from "store/root/organization/candidature/types";
import { RootState } from "store/types";
import produce, { Draft } from "immer";

interface StateProps {
  candidatures: ClanCandidaturesState;
  ownProps: OwnProps;
}

interface OwnProps {
  type: string;
  id: string;
}

export const withCandidatures = (
  HighOrderComponent: FunctionComponent<any>
) => {
  const Container: FunctionComponent<any> = ({
    actions,
    candidatures,
    ownProps,
    ...attributes
  }) => {
    useEffect(() => {
      switch (ownProps.type) {
        case "user":
          if (
            !candidatures.data.some(
              candidature => candidature.userId === ownProps.id
            )
          ) {
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
    return (
      <HighOrderComponent
        actions={actions}
        candidatures={candidatures}
        {...attributes}
      />
    );
  };

  const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
    state,
    ownProps
  ) => {
    const candidatures = produce(
      state.root.organization.candidature,
      (draft: Draft<ClanCandidaturesState>) => {
        draft.data.forEach(candidature => {
          candidature.clan =
            state.root.organization.clan.data.find(
              clan => clan.id === candidature.clanId
            ) || null;
        });
      }
    );
    return {
      candidatures,
      ownProps // NOT GOOD
    };
  };

  const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
    return {
      actions: {
        loadCandidatures: () =>
          dispatch(loadClanCandidatures(ownProps.type, ownProps.id)),
        loadCandidature: (candidatureId: string) =>
          dispatch(loadClanCandidature(candidatureId)),
        acceptCandidature: (candidatureId: string) =>
          dispatch(acceptClanCandidature(candidatureId)).then(
            loadClanCandidatures(ownProps.type, ownProps.id)
          ),
        declineCandidature: (candidatureId: string) =>
          dispatch(declineClanCandidature(candidatureId)).then(
            loadClanCandidatures(ownProps.type, ownProps.id)
          ),
        addCandidature: (clanId: string, userId: string) =>
          dispatch(sendClanCandidature(clanId, userId)).then(
            loadClanCandidatures(ownProps.type, ownProps.id)
          )
      }
    };
  };

  return connect(mapStateToProps, mapDispatchToProps)(Container);
};
