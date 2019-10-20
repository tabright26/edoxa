import React, { useEffect, useState, FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";

import { withClans } from "store/root/organizations/clans/container";

import CandidatureWidget from "components/Organization/Clan/Candidature/Widget/Widget";
import ClanInfo from "components/Organization/Clan/Summary/Summary";

import ErrorBoundary from "components/Shared/ErrorBoundary";

import { compose } from "recompose";

const ClanDetailsIndex: FunctionComponent<any> = ({
  actions,
  clans,
  userId,
  userClan,
  match: {
    params: { clanId }
  }
}) => {
  const [clan, setClan] = useState(null);

  useEffect(() => {
    if (!clans.some(clan => clan.id === clanId)) {
      actions.loadClan(clanId);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clanId]);

  useEffect(() => {
    setClan(clans.find(clan => clan.id === clanId));
  }, [clanId, clans]);

  return (
    <ErrorBoundary>
      {clan && (
        <Card>
          <CardHeader>
            <ClanInfo clan={clan} />
          </CardHeader>
          <CardBody>{!userClan ? <CandidatureWidget type="user" id={userId} clanId={clanId} userId={userId} /> : null}</CardBody>
        </Card>
      )}
    </ErrorBoundary>
  );
};

const enhance = compose<any, any>(withClans);

export default enhance(ClanDetailsIndex);
