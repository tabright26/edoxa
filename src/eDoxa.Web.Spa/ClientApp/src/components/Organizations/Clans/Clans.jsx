import React, { useState, useEffect } from "react";
import { Row, Col, Card, CardHeader } from "reactstrap";
import { connectClans } from "store/organizations/clans/container";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus } from "@fortawesome/free-solid-svg-icons";

import ClansItem from "./ClanItem";

import ClanModal from "modals/Organizations/Clan";

import ErrorBoundary from "components/Shared/ErrorBoundary";

const ClansIndex = ({ actions, clans, userClanId }) => {
  const [searchValue, setSearchValue] = useState("");

  useEffect(() => {
    actions.loadClans();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  userClanId = Date.now() % 2 === 0;

  const handleSearchInputChanges = e => {
    setSearchValue(e.target.value);
  };

  return (
    <ErrorBoundary>
      <Row>
        <Col>
          <Card className="mt-4">
            <CardHeader tag="h5" className="text-center">
              <Row>
                <Col>
                  <small className="text-muted">{clans.length} clans and counting... You can also create your own !</small>
                  {userClanId ? (
                    <div className="card-header-actions btn-link" onClick={() => actions.showCreateAddressModal()}>
                      <FontAwesomeIcon icon={faPlus} />
                    </div>
                  ) : (
                    <div></div>
                  )}
                  <ClanModal.Create actions={actions}></ClanModal.Create>
                </Col>
                <Col>Clans</Col>
                <Col>
                  Search: <input type="text" value={searchValue} onChange={handleSearchInputChanges} />
                </Col>
              </Row>
            </CardHeader>
          </Card>
        </Col>
      </Row>
      <Row>
        {clans
          .filter(clan => (searchValue ? clan.name.includes(searchValue) : clan))
          .map((clan, index) => (
            <Col key={index} xs="6" sm="4" md="3">
              <ClansItem clan={clan} />
            </Col>
          ))}
      </Row>
    </ErrorBoundary>
  );
};

export default connectClans(ClansIndex);
