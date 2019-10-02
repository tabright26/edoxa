import React, { useEffect } from "react";
import { Row, Col, Card, CardHeader, CardBody } from "reactstrap";
import { connectClan } from "store/organizations/clan/container";

import ClanInfo from "./ClanInfo";
import ClanStats from "./ClanStats";
import Candidatures from "./Candidatures";
import Members from "./Members";

import ErrorBoundary from "components/Shared/ErrorBoundary";

const candidatures = [{ doxaTag: "User1" }, { doxaTag: "User2" }, { doxaTag: "User3" }];

const clan = {
  name: "Big Boi Clan",
  summary: "This is a summary. We do esport and shit man.",
  ownerId: "Owner DoxaTag: Gabroney",
  members: [
    {
      doxaTag: "Member1"
    },
    {
      doxaTag: "Member2"
    },
    {
      doxaTag: "Member3"
    },
    {
      doxaTag: "Member4"
    },
    {
      doxaTag: "Member5"
    },
    {
      doxaTag: "Member6"
    },
    {
      doxaTag: "Member7"
    },
    {
      doxaTag: "Member8"
    }
  ]
};

const ClanDashboardIndex = ({ actions }) => {
  useEffect(() => {
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
    <ErrorBoundary>
      <Row>
        <Col>
          <Card className="mt-4">
            <CardHeader tag="h3" className="text-center">
              {clan.name} Dashboard
            </CardHeader>
            <CardBody>
              <Row>
                <Col xs="3" sm="3" md="3">
                  <ClanInfo info={{ name: clan.name, summary: clan.summary }} />
                </Col>
                <Col xs="2" sm="2" md="2">
                  <Members members={clan.members} />
                </Col>
                <Col xs="2" sm="2" md="2">
                  <Candidatures candidatures={candidatures} />
                </Col>
                <Col xs="3" sm="3" md="3">
                  <ClanStats />
                </Col>
              </Row>
            </CardBody>
          </Card>
        </Col>
      </Row>
    </ErrorBoundary>
  );
};

export default connectClan(ClanDashboardIndex);
