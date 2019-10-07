import React, { useEffect, useState } from "react";
import { Col, Card, CardHeader, CardBody } from "reactstrap";

import { connectClans } from "store/organizations/clans/container";

import ClanInfo from "components/Organizations/Clans/ClanInfo";
import CandidatureWidget from "components/Organizations/Candidatures/CandidatureWidget";

import ErrorBoundary from "components/Shared/ErrorBoundary";

const ClanDetailsIndex = ({
  actions,
  clans,
  userId,
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
      <Card>
        <CardHeader>
          <Col>Clan Details</Col>
          <Col>Clan Id: {clan ? clan.id : ""}</Col>
          <Col>Clan Owner Id: {clan ? clan.ownerId : ""}</Col>
          <Col>{clan ? <CandidatureWidget clanId={clan.id} userId={userId} /> : ""}</Col>
        </CardHeader>
        <CardBody>{clan ? <ClanInfo clan={clan} /> : ""}</CardBody>
      </Card>
    </ErrorBoundary>
  );
};

export default connectClans(ClanDetailsIndex);
