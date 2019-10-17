import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadClans, loadClan, createClan } from "store/root/organizations/clans/actions";
import { downloadClanLogo, uploadClanLogo } from "store/root/organizations/logos/actions";
import { RootState } from "store/root/types";

export const withClans = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect(() => {
      props.actions.loadClans();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    const clans = state.organizations.clans.data.map(clan => {
      const doxatag = state.doxatags.data.find(doxatag => doxatag.userId === clan.ownerId);

      clan.ownerDoxatag = doxatag ? doxatag.name + "#" + doxatag.code : null;
      return clan;
    });

    const userId = state.oidc.user.profile.sub;

    return {
      clans,
      userId,
      userClan: clans.find(clan => clan.members.find(member => member.userId === userId))
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadClans: () => dispatch(loadClans()),
        loadClan: (clanId: string) => dispatch(loadClan(clanId)),
        addClan: (data: any) => dispatch(createClan(data)).then(loadClans()),
        loadLogo: (clanId: string) => dispatch(downloadClanLogo(clanId)),
        updateLogo: (clanId: string, data: any) => dispatch(uploadClanLogo(clanId, data))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
